using Application.Modules.GradesModule.Commands.SubmitGrade;
using Application.Modules.GradesModule.Queries.GetCurrentSuccess;
using Application.Modules.GradesModule.Queries.GetGradesByLesson;
using Application.Modules.LecturesModule.Queries.GetLecturesByLesson;
using Application.Modules.TranscriptsModule.Queries.GetStudentTranscript;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    public class GradesController : AdminBaseController
    {
        private readonly IMediator mediator;

        public GradesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// GET: /Admin/Grades?lessonId=5
        /// Teacher's grading table for a specific lesson.
        /// </summary>
        public async Task<IActionResult> Index(int lessonId)
        {
            ViewBag.LessonId = lessonId;
            var grades = await mediator.Send(new GetGradesByLessonQuery { LessonId = lessonId });
            return View(grades);
        }

        /// <summary>
        /// POST: /Admin/Grades/SubmitGrade
        /// AJAX endpoint for saving a single student grade.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SubmitGrade([FromBody] SubmitGradeCommand command)
        {
            var result = await mediator.Send(command);
            if (!result)
                return BadRequest(new { message = "Cannot modify finalized grades." });

            return Ok(new { message = "Grade saved successfully." });
        }

        /// <summary>
        /// GET: /Admin/Grades/CurrentSuccess?studentId=1&lessonId=5
        /// Real-time "Cari Müvəffəqiyyət" dashboard for a student.
        /// </summary>
        public async Task<IActionResult> CurrentSuccess(int studentId, int lessonId)
        {
            var data = await mediator.Send(new GetCurrentSuccessQuery
            {
                StudentId = studentId,
                LessonId = lessonId
            });
            return View(data);
        }

        /// <summary>
        /// GET: /Admin/Grades/Transcript?studentId=1
        /// Full academic transcript view.
        /// </summary>
        public async Task<IActionResult> Transcript(int studentId)
        {
            var transcript = await mediator.Send(new GetStudentTranscriptQuery
            {
                StudentId = studentId
            });
            return View(transcript);
        }

        /// <summary>
        /// GET: /Admin/Grades/Lectures?lessonId=5
        /// Lecture list with downloadable materials.
        /// </summary>
        public async Task<IActionResult> Lectures(int lessonId)
        {
            ViewBag.LessonId = lessonId;
            var lectures = await mediator.Send(new GetLecturesByLessonQuery { LessonId = lessonId });
            return View(lectures);
        }
    }
}
