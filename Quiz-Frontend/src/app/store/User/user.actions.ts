

import { createAction, props } from '@ngrx/store';
import { Attender } from "../../Models/UserModel";

export const loadAttenderProfile = createAction(
  '[Attender] Load Attender Profile',
  props<{ attenderId: string }>()
);

export const loadAttenderProfileSuccess = createAction(
  '[Attender] Load Attender Profile Success',
  props<{ profile: Attender }>()
);

export const loadAttenderProfileFailure = createAction(
  '[Attender] Load Attender Profile Failure',
  props<{ error: string }>()
);
