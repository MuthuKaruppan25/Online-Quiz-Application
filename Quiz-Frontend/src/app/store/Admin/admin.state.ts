import { Admin } from "../../Models/UserModel";

export interface AdminState {
  profile: Admin | null;
  loading: boolean;
  error: string | null;
}

export const initialAdminState: AdminState = {
  profile: null,
  loading: false,
  error: null,
};
