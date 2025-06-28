export class AttemptAddDto {
  attemptorId: string = '';        
  quizId: string = '';            
  timeTakenMins: number = 0;
  autoSubmitted: boolean = false;
  answers?: AnswerAddDto[];
}

export class AnswerAddDto {
  questionId: string = '';        
  selectedOption: string = '';
}

