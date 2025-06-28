export class QuestionsAddDto {
  text: string = '';
  optionA: string = '';
  optionB: string = '';
  optionC: string = '';
  optionD: string = '';
  correctOption: string = '';
  explanation: string = '';

  constructor(init?: Partial<QuestionsAddDto>) {
    Object.assign(this, init);
  }
}


export class QuizAddDto {
  title: string = '';
  description: string = '';
  durationMinutes: number = 0;
  adminId: string = '';
  categoryId: string = '';
  questions?: QuestionsAddDto[];

  constructor(init?: Partial<QuizAddDto>) {
    Object.assign(this, init);
  }
}
