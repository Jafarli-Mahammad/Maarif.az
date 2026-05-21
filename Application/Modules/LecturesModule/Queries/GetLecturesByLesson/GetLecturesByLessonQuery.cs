using MediatR;
using System.Collections.Generic;

namespace Application.Modules.LecturesModule.Queries.GetLecturesByLesson
{
    /// <summary>
    /// Query to retrieve all lectures for a lesson, ordered by sequence.
    /// Used in the "Dərs Materialları" tab.
    /// </summary>
    public class GetLecturesByLessonQuery : IRequest<List<LectureResponseDto>>
    {
        public int LessonId { get; set; }
    }

    public class LectureResponseDto
    {
        public int Id { get; set; }
        public int OrderIndex { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime LectureDate { get; set; }
        public int DurationMinutes { get; set; }
        public string Type { get; set; }               // Mühazirə, Seminar, Laboratoriya
        public string TeacherName { get; set; }
        public int MaterialCount { get; set; }

        // Nested materials
        public List<LectureMaterialResponseDto> Materials { get; set; }
    }

    public class LectureMaterialResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string Type { get; set; }               // PDF, Video, etc.
        public int DownloadCount { get; set; }
    }
}
