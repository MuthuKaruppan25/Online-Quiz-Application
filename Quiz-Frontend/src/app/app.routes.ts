import { Routes } from '@angular/router';
import { LandingPage } from './landing-page/landing-page';
import { Login } from './login/login';
import { Dashboard } from './dashboard/dashboard';
import { CreateQuiz } from './create-quiz/create-quiz';
import { QuestionCreateCard } from './question-create-card/question-create-card';
import { ViewQuiz } from './view-quiz/view-quiz';
import { QuestionAnswer } from './question-answer/question-answer';
import { ViewAnswer } from './view-answer/view-answer';
import { UserHistory } from './user-history/user-history';
import { Myquiz } from './myquiz/myquiz';
import { AdminAttempts } from './admin-attempts/admin-attempts';
import { AdminDashboard } from './admin-dashboard/admin-dashboard';
import { UserDashboard } from './user-dashboard/user-dashboard';
import { ProfileUser } from './profile-user/profile-user';

export const routes: Routes = [
  {
    path: 'home',
    component: LandingPage,
  },
  {
    path: 'login',
    component: Login,
  },
  {
    path: 'dashboard',
    component: Dashboard,
    children: [
      {
        path: 'create',
        component: CreateQuiz,
      },
      {
        path:'add/questions',
        component:QuestionCreateCard
      },
      {
        path:'view/quiz',
        component:ViewQuiz
      },
      {
        path:'take/quiz',
        component: QuestionAnswer
      },
      {
        path:'view/score',
        component:ViewAnswer
      },
      {
        path: 'view/history',
        component:UserHistory
      },
      {
        path:'myQuiz',
        component:Myquiz
      },
      {
        path:'view/attempts',
        component:AdminAttempts
      },
      {
        path:'admin/dashboard',
        component:AdminDashboard
      },
      {
        path:'user/dashboard',
        component:UserDashboard
      },
      {
        path:'user/profile',
        component:ProfileUser
      }

    ],
  },
  
];
