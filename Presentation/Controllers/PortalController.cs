using Application.Modules.StudentsModule.Queries.GetStudentPortalProfileQuery;
using Application.Modules.StudentsModule.Queries.GetStudentScheduleQuery;
using Application.Modules.SubjectsModule.Queries.PortalSubjectQuery;
using Application.Repositories;
using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.AppCode;
using Presentation.AppCode.Extensions;
using Presentation.AppCode.ViewModels;

namespace Presentation.Controllers
{
    [Authorize(Roles = "STUDENT")]
    public class PortalController : Controller
    {
        private readonly IMediator mediator;
        private readonly IStudentRepository studentRepository;

        public PortalController(IMediator mediator, IStudentRepository studentRepository)
        {
            this.mediator = mediator;
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var userId = User.GetRequiredUserId();
            var profile = await mediator.Send(
                new GetStudentPortalProfileRequest { UserId = userId },
                cancellationToken);

            if (profile is null)
                return RedirectToAction(nameof(AuthController.Login), "Auth");

            var schedule = await mediator.Send(
                new GetStudentScheduleRequest { UserId = userId },
                cancellationToken);

            var vm = new PortalViewModel
            {
                FullName = profile.FullName,
                StudentNumber = profile.StudentNumber,
                CurrentWeekType = schedule.CurrentWeekType,
                Days = schedule.Days
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Profile(CancellationToken cancellationToken)
        {
            var userId = User.GetRequiredUserId();
            var profile = await mediator.Send(
                new GetStudentPortalProfileRequest { UserId = userId },
                cancellationToken);

            if (profile is null)
                return RedirectToAction(nameof(AuthController.Login), "Auth");

            return View(profile);
        }

        [HttpGet]
        public async Task<IActionResult> Subjects(CancellationToken cancellationToken)
        {
            var userId = User.GetRequiredUserId();
            var profile = await mediator.Send(
                new GetStudentPortalProfileRequest { UserId = userId },
                cancellationToken);

            if (profile is null)
                return RedirectToAction(nameof(AuthController.Login), "Auth");

            PopulateStudentSubjectViewBag(profile, "Fənlər", "Fənlər");

            var nav = await mediator.Send(
                new GetPortalSubjectNavRequest { UserId = userId, ForTeacher = false },
                cancellationToken);

            if (nav.Count == 0)
                return View("/Views/Shared/PortalNoSubjects.cshtml");

            return RedirectToAction(nameof(Subject), new { id = nav[0].Id });
        }

        [HttpGet]
        public async Task<IActionResult> Subject(int id, CancellationToken cancellationToken)
        {
            var userId = User.GetRequiredUserId();
            var profile = await mediator.Send(
                new GetStudentPortalProfileRequest { UserId = userId },
                cancellationToken);

            if (profile is null)
                return RedirectToAction(nameof(AuthController.Login), "Auth");

            try
            {
                var workspace = await mediator.Send(
                    new GetPortalSubjectWorkspaceRequest
                    {
                        UserId = userId,
                        SubjectId = id,
                        ForTeacher = false
                    },
                    cancellationToken);

                PopulateStudentSubjectViewBag(profile, workspace.Subject.Name, workspace.Subject.Name);

                var vm = new SubjectWorkspacePageViewModel
                {
                    Workspace = workspace,
                    IsTeacher = false
                };

                return View("/Views/Shared/SubjectWorkspace.cshtml", vm);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Assignments(int subjectId, CancellationToken cancellationToken)
        {
            var userId = User.GetRequiredUserId();
            var profile = await mediator.Send(
                new GetStudentPortalProfileRequest { UserId = userId },
                cancellationToken);

            if (profile is null)
                return RedirectToAction(nameof(AuthController.Login), "Auth");

            PopulateStudentSubjectViewBag(profile, "Tapşırıqlar", "Tapşırıqlar");
            ViewBag.SubjectId = subjectId;

            var workspace = await mediator.Send(new GetPortalSubjectWorkspaceRequest { UserId = userId, SubjectId = subjectId, ForTeacher = false }, cancellationToken);

            var assignments = new List<Application.Modules.AssignmentsModule.Queries.GetAssignmentsByLesson.AssignmentDto>();
            if (workspace?.Subject?.Lessons != null)
            {
                foreach (var lesson in workspace.Subject.Lessons)
                {
                    var lessonAssignments = await mediator.Send(new Application.Modules.AssignmentsModule.Queries.GetAssignmentsByLesson.GetAssignmentsByLessonQuery { LessonId = lesson.Id }, cancellationToken);
                    assignments.AddRange(lessonAssignments);
                }
            }

            return View(assignments.DistinctBy(a => a.Id).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Assignment(int id, int subjectId, CancellationToken cancellationToken)
        {
            var userId = User.GetRequiredUserId();
            var profile = await mediator.Send(
                new GetStudentPortalProfileRequest { UserId = userId },
                cancellationToken);

            if (profile is null)
                return RedirectToAction(nameof(AuthController.Login), "Auth");

            // Fetch assignments to find the one we want
            var workspace = await mediator.Send(new GetPortalSubjectWorkspaceRequest { UserId = userId, SubjectId = subjectId, ForTeacher = false }, cancellationToken);
            
            Application.Modules.AssignmentsModule.Queries.GetAssignmentsByLesson.AssignmentDto assignment = null;
            if (workspace?.Subject?.Lessons != null)
            {
                foreach (var lesson in workspace.Subject.Lessons)
                {
                    var lessonAssignments = await mediator.Send(new Application.Modules.AssignmentsModule.Queries.GetAssignmentsByLesson.GetAssignmentsByLessonQuery { LessonId = lesson.Id }, cancellationToken);
                    assignment = lessonAssignments.FirstOrDefault(a => a.Id == id);
                    if (assignment != null) break;
                }
            }

            if (assignment == null)
                return NotFound();

            PopulateStudentSubjectViewBag(profile, assignment.Title, "Tapşırıq");
            ViewBag.SubjectId = subjectId;
            ViewBag.Workspace = workspace;

            return View(assignment);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAssignment(Application.Modules.SubmissionsModule.Commands.SubmitAssignment.SubmitAssignmentCommand command, int subjectId, CancellationToken cancellationToken)
        {
            var userId = User.GetRequiredUserId();
            var student = await studentRepository.GetByUserIdWithDetailsAsync(userId, cancellationToken);
            if (student == null)
            {
                return NotFound("Tələbə tapılmadı.");
            }
            command.StudentId = student.Id;

            try
            {
                await mediator.Send(command, cancellationToken);
                TempData["SuccessMessage"] = "Tapşırıq uğurla göndərildi.";
                return RedirectToAction(nameof(Assignment), new { id = command.AssignmentId, subjectId = subjectId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Assignment), new { id = command.AssignmentId, subjectId = subjectId });
            }
        }

        private void PopulateStudentSubjectViewBag(
            StudentPortalProfileDto profile,
            string title,
            string breadcrumb)
        {
            ViewBag.Title = title;
            ViewBag.PortalFullName = profile.FullName;
            ViewBag.PortalBadge = profile.StudentNumber;
            ViewBag.PortalInitials = PortalText.InitialsFrom(profile.FullName);
            ViewBag.PortalActiveNav = "subjects";
            ViewBag.PortalIsTeacher = false;
            ViewBag.PortalBreadcrumb = breadcrumb;
        }
    }
}
