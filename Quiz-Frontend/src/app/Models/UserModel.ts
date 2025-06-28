export class UserRegister {
  username: string = '';
  password: string = '';
  firstName: string = '';
  lastName: string = '';
  role: string = '';

  constructor(init?: Partial<UserRegister>) {
    Object.assign(this, init);
  }
}


export class Admin {
  guid: string = ''; 
  firstName: string = '';
  lastName: string = '';

  constructor(init?: Partial<Admin>) {
    Object.assign(this, init);
  }
}


export class Attender {
  guid: string = '';
  firstName: string = '';
  lastName: string = '';

  constructor(init?: Partial<Attender>) {
    Object.assign(this, init);
  }
}
