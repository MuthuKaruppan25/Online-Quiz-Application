public class QuestionsMapper
{
    public Question MaptoQuestion(QuestionsAddDto questionData, string quizId)
    {
        Question question = new()
        {
            guid = Guid.NewGuid(),
            Text = questionData.Text,
            OptionA = questionData.OptionA,
            OptionB = questionData.OptionB,
            OptionC = questionData.OptionC,
            OptionD = questionData.OptionD,
            CorrectOption = questionData.CorrectOption,
            Explanation = questionData.Explanation,
            QuizId = quizId
        };
        return question;
    }
}
