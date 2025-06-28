namespace Quiz.Models;


public class Category
{
    public Guid guid { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public ICollection<QuizData>? quizDatas { get; set; }
    
    
}