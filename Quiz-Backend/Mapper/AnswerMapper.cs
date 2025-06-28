

using Quiz.Models;

public class AnswerMapper
{
    public Answers MaptoAnswer(AnswerAddDto answerAddDto, bool stat, Guid attemptId)
    {
        Answers answers = new();
        answers.guid = Guid.NewGuid();
        answers.IsCorrect = stat;
        answers.QuestionId = answerAddDto.QuestionId;
        answers.quizAttemptId = attemptId;
        answers.SelectedOption = answerAddDto.SelectedOption;
        return answers;
    }
}