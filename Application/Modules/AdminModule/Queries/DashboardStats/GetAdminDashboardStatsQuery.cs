using Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Modules.AdminModule.Queries.DashboardStats;

public class AdminDashboardStatsDto
{
    public int TotalStudents { get; set; }
    public int TotalTeachers { get; set; }
    public int TotalFaculties { get; set; }
    public int TotalDepartments { get; set; }
    public int TotalGroups { get; set; }
    public int TotalSubjects { get; set; }
    public int TotalLessons { get; set; }
}

public class GetAdminDashboardStatsQuery : IRequest<AdminDashboardStatsDto>
{
}

public class GetAdminDashboardStatsQueryHandler : IRequestHandler<GetAdminDashboardStatsQuery, AdminDashboardStatsDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IFacultyRepository _facultyRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ILessonRepository _lessonRepository;

    public GetAdminDashboardStatsQueryHandler(
        IStudentRepository studentRepository,
        ITeacherRepository teacherRepository,
        IFacultyRepository facultyRepository,
        IDepartmentRepository departmentRepository,
        IGroupRepository groupRepository,
        ISubjectRepository subjectRepository,
        ILessonRepository lessonRepository)
    {
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
        _facultyRepository = facultyRepository;
        _departmentRepository = departmentRepository;
        _groupRepository = groupRepository;
        _subjectRepository = subjectRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<AdminDashboardStatsDto> Handle(GetAdminDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        return new AdminDashboardStatsDto
        {
            TotalStudents = await _studentRepository.GetAll().CountAsync(cancellationToken),
            TotalTeachers = await _teacherRepository.GetAll().CountAsync(cancellationToken),
            TotalFaculties = await _facultyRepository.GetAll().CountAsync(cancellationToken),
            TotalDepartments = await _departmentRepository.GetAll().CountAsync(cancellationToken),
            TotalGroups = await _groupRepository.GetAll().CountAsync(cancellationToken),
            TotalSubjects = await _subjectRepository.GetAll().CountAsync(cancellationToken),
            TotalLessons = await _lessonRepository.GetAll().CountAsync(cancellationToken)
        };
    }
}
