export class QuizAttemptsSummaryDto {
  quizId: string;
  quizTitle: string;
  totalAttempts: number;
  attemptsAbove50Percent: number;
  attemptsBelowOrEqual50Percent: number;

  constructor(
    quizId: string = '',
    quizTitle: string = '',
    totalAttempts: number = 0,
    attemptsAbove50Percent: number = 0,
    attemptsBelowOrEqual50Percent: number = 0
  ) {
    this.quizId = quizId;
    this.quizTitle = quizTitle;
    this.totalAttempts = totalAttempts;
    this.attemptsAbove50Percent = attemptsAbove50Percent;
    this.attemptsBelowOrEqual50Percent = attemptsBelowOrEqual50Percent;
  }
}

export class AdminQuizAttemptsSummaryResponseDto {
  quizSummaries: QuizAttemptsSummaryDto[];

  constructor(quizSummaries: QuizAttemptsSummaryDto[] = []) {
    this.quizSummaries = quizSummaries;
  }
}
