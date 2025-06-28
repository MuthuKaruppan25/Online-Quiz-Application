import { Attender } from "../../Models/UserModel";

export interface AttenderState {
  profile: Attender | null;
  loading: boolean;
  error: string | null;
}

export const initialAttenderState: AttenderState = {
  profile: null,
  loading: false,
  error: null,
};