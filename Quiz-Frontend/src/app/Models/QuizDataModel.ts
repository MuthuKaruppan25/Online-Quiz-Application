export class QuizData {
  id: string = '';
  title: string = '';
  description: string = '';
  durationMinutes: number = 0;
  adminId: string = '';
  categoryId: string = '';
  createdAt: string = '';
  isDeleted: boolean = false;
  questions?: Question[];

  constructor(init?: Partial<QuizData>) {
    Object.assign(this, init);
  }
}

export class Question {
  guid: string = '';
  text: string = '';
  optionA: string = '';
  optionB: string = '';
  optionC: string = '';
  optionD: string = '';
  correctOption: string = '';
  explanation: string = '';
  quizId: string = '';

  constructor(init?: Partial<Question>) {
    Object.assign(this, init);
  }
}
