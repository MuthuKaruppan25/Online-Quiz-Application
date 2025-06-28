public class QuestionResMapper
{
    public QuizAddResponse MapToResponse(QuizData quizData)
    {
        QuizAddResponse response = new()
        {
            Id = quizData.Id,
            Title = quizData.Title,
            Description = quizData.Description,
            durationMinutes = quizData.durationMinutes,
            adminId = quizData.adminId,
            categoryId = quizData.categoryId
        };
        return response;
    }
}
