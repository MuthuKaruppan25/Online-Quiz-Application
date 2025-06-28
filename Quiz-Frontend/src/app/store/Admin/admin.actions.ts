import { createAction, props } from '@ngrx/store';
import { Admin } from '../../Models/UserModel';

export const loadAdminProfile = createAction(
  '[Admin] Load Admin Profile',
  props<{ adminId: string }>()
);

export const loadAdminProfileSuccess = createAction(
  '[Admin] Load Admin Profile Success',
  props<{ profile: Admin }>()
);

export const loadAdminProfileFailure = createAction(
  '[Admin] Load Admin Profile Failure',
  props<{ error: string }>()
);
