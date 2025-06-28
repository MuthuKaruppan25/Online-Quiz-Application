import { Answers } from "./QuizAttempt";
import { QuizData } from "./QuizDataModel";

export class Attempts {
  guid: string = '';
  attemptorId: string = '';
  quizId: string = '';
  timeTakenMins: number = 0;
  attemptedAt: string = ''; 
  autoSubmitted: boolean = false;
  score: number = 0;
  quizData!:QuizData;
  answers!: Answers[];

  constructor(init?: Partial<Attempts>) {
    Object.assign(this, init);
  }
}

