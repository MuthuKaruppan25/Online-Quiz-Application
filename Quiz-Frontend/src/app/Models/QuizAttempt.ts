export class QuizAttempt {
  guid: string = '';
  attemptorId: string = '';
  quizId: string = '';
  timeTakenMins: number = 0;
  attemptedAt: string = '';   // you can store DateTime as ISO string
  autoSubmitted: boolean = false;
  score: number = 0;
  answers?: Answers[];        // no question / quizData navigation property

  constructor(init?: Partial<QuizAttempt>) {
    Object.assign(this, init);
  }
}

export class Answers {
  guid: string = '';
  questionId: string = '';
  selectedOption: string = '';
  isCorrect: boolean = false;
  quizAttemptId: string = ''; 

  constructor(init?: Partial<Answers>) {
    Object.assign(this, init);
  }
}
