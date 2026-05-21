namespace Domain.Models.Stables
{
    /// <summary>
    /// The pedagogical type of a lecture session.
    /// Semantically distinct from <see cref="LessonType"/> which is used for scheduling.
    /// </summary>
    public enum LectureType
    {
        Mühazirə = 1,     // Lecture (theory)
        Seminar = 2,       // Seminar / Practice session
        Laboratoriya = 3   // Laboratory session
    }
}
