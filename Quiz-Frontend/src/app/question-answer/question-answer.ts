import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AttemptAddDto, AnswerAddDto } from '../Models/AttemptModel';
import { QuizData, Question } from '../Models/QuizDataModel';
import { Attender } from '../Models/UserModel';
import { AttemptService } from '../services/AttemptService';
import { Store } from '@ngrx/store';
import { selectAttenderProfile } from '../store/User/user.selectors';
import { FormsModule } from '@angular/forms';
import { QuizService } from '../services/QuizService';
import { SnackBar } from '../snack-bar/snack-bar';
import { QuizAttempt } from '../Models/QuizAttempt';

@Component({
  selector: 'app-question-answer',
  imports: [FormsModule,SnackBar],
  templateUrl: './question-answer.html',
  styleUrls: ['./question-answer.css'],
})
export class QuestionAnswer implements OnInit {
  quizData!: QuizData;
  currentQuestionIndex = 0;
  currentQuestion!: Question;
  selectedOption: string = '';
  answers: AnswerAddDto[] = [];
  timer: number = 0; // seconds
  intervalId: any;
  submitted: boolean = false;
  quizAttempt!:QuizAttempt;
  snackMessage: string = '';
  snackSuccess: boolean = true;

  attenderData!: Attender;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private quizService: QuizService,
    private attemptService: AttemptService,
    private store: Store
  ) {}

  ngOnInit(): void {
    const stateData = history.state.quiz as QuizData;
    if (!stateData) {
      this.router.navigate(['/']);
      return;
    }
    this.quizData = stateData;
    this.currentQuestion = this.quizData.questions![this.currentQuestionIndex];
    this.timer = this.quizData.durationMinutes * 60;

    // subscribe to attender profile from store
    this.store.select(selectAttenderProfile).subscribe({
      next: (data: any) => {
        this.attenderData = data as Attender;
        console.log(this.attenderData);
      },
    });

    this.startTimer();
  }

  startTimer() {
    this.intervalId = setInterval(() => {
      this.timer--;
      if (this.timer <= 0) {
        clearInterval(this.intervalId);
        this.autoSubmit();
      }
    }, 1000);
  }

  saveAndNext() {
    this.saveCurrentAnswer();

    if (this.currentQuestionIndex < this.quizData.questions!.length - 1) {
      this.currentQuestionIndex++;
      this.currentQuestion =
        this.quizData.questions![this.currentQuestionIndex];
      this.selectedOption = this.getSelectedOption(this.currentQuestion.guid);
    } else {
      this.submitTest();
    }
  }

  getSelectedOption(questionId: string) {
    const found = this.answers.find((a) => a.questionId === questionId);
    return found ? found.selectedOption : '';
  }

  saveCurrentAnswer() {
    const existing = this.answers.find(
      (a) => a.questionId === this.currentQuestion.guid
    );
    if (existing) {
      existing.selectedOption = this.selectedOption;
    } else {
      this.answers.push({
        questionId: this.currentQuestion.guid,
        selectedOption: this.selectedOption,
      });
    }
  }

  submitTest() {
    clearInterval(this.intervalId);
    this.saveCurrentAnswer();

    const attempt: AttemptAddDto = {
      attemptorId: this.attenderData.guid,
      quizId: this.quizData.id,
      timeTakenMins: Math.floor(
        this.quizData.durationMinutes - this.timer / 60
      ),
      autoSubmitted: false,
      answers: this.answers,
    };

    this.attemptService.addAttempt(attempt).subscribe({
      next: (data:any) => {
        this.showSnack('Test submitted successfully!', true);
        console.log("Test Submitted Succesfully");
        this.quizAttempt = data as QuizAttempt;
        console.log(this.quizAttempt);
        this.submitted = true;
      },
      error: (err) => {
        console.error(err);
        this.showSnack('Error submitting test: ' + err.message, false);
      },
    });
  }

  autoSubmit() {
    // mark unanswered questions as empty
    this.quizData.questions!.forEach((q) => {
      if (!this.answers.find((a) => a.questionId === q.guid)) {
        this.answers.push({
          questionId: q.guid,
          selectedOption: '',
        });
      }
    });

    const attempt: AttemptAddDto = {
      attemptorId: this.attenderData.guid,
      quizId: this.quizData.id,
      timeTakenMins: this.quizData.durationMinutes,
      autoSubmitted: true,
      answers: this.answers,
    };

    this.attemptService.addAttempt(attempt).subscribe({
      next: (data:any) => {
        this.showSnack('Test auto-submitted due to timeout!', true);
        this.submitted = true;
        this.quizAttempt = data as QuizAttempt;
      },
      error: (err) => {
        console.error(err);
        this.showSnack('Error auto-submitting: ' + err.message, false);
      },
    });
  }

  get timeDisplay() {
    const min = Math.floor(this.timer / 60);
    const sec = this.timer % 60;
    return `${min}:${sec < 10 ? '0' + sec : sec}`;
  }

  private showSnack(message: string, success: boolean) {
    this.snackMessage = message;
    this.snackSuccess = success;
  }
  viewScore(){
    this.router.navigate(['/dashboard/view/score'],{
      state:{
        quizAttempt  : this.quizAttempt,
        quiz : this.quizData
      }
    })
  }
}
