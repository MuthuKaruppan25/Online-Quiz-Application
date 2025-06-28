import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { AdminService } from './services/AdminService';
import { UserService } from './services/UserService';
import { provideState, provideStore } from '@ngrx/store';
import { AdminReducer } from './store/Admin/admin.reducers';
import { AttenderReducer } from './store/User/user.reducers';
import { CategoryService } from './services/CategoryService';
import { QuizService } from './services/QuizService';
import { AttemptService } from './services/AttemptService';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    UserService,
    AdminService,
    provideStore(),
    provideState('admin',AdminReducer),
    provideState('attender',AttenderReducer),
    CategoryService,
    QuizService,
    AttemptService

  ]
};
