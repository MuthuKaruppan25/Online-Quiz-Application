export class AttenderIndividualAttemptSummaryDto {
  quizId: string = '';
  quizTitle: string = '';
  score: number = 0;
  isAbove50Percent: boolean = false;
  attemptedAt: Date = new Date();

  constructor(init?: Partial<AttenderIndividualAttemptSummaryDto>) {
    Object.assign(this, init);
  }
}

export class AttenderIndividualAttemptsResponseDto {
  attempts: AttenderIndividualAttemptSummaryDto[] = [];

  constructor(init?: Partial<AttenderIndividualAttemptsResponseDto>) {
    Object.assign(this, init);
  }
}
