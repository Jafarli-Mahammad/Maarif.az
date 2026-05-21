using Application.Repositories;
using MediatR;

namespace Application.Modules.LecturesModule.Queries.GetLecturesByLesson
{
    public class GetLecturesByLessonQueryHandler
        : IRequestHandler<GetLecturesByLessonQuery, List<LectureResponseDto>>
    {
        private readonly ILectureRepository _lectureRepository;

        public GetLecturesByLessonQueryHandler(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
        }

        public async Task<List<LectureResponseDto>> Handle(
            GetLecturesByLessonQuery request, CancellationToken cancellationToken)
        {
            var lectures = await _lectureRepository.GetByLessonIdAsync(
                request.LessonId, cancellationToken);

            return lectures.Select(l => new LectureResponseDto
            {
                Id = l.Id,
                OrderIndex = l.OrderIndex,
                Title = l.Title,
                Description = l.Description,
                LectureDate = l.LectureDate,
                DurationMinutes = l.DurationMinutes,
                Type = l.Type.ToString(),
                TeacherName = l.Lesson?.Teacher?.FullName ?? "",
                MaterialCount = l.Materials?.Count ?? 0,
                Materials = l.Materials?.Select(m => new LectureMaterialResponseDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    FileName = m.FileName,
                    FilePath = m.FilePath,
                    ContentType = m.ContentType,
                    FileSize = m.FileSize,
                    Type = m.Type.ToString(),
                    DownloadCount = m.DownloadCount
                }).ToList() ?? new List<LectureMaterialResponseDto>()
            }).ToList();
        }
    }
}
