

export class Category {
  guid: string = '';
  name: string = '';

  constructor(init?: Partial<Category>) {
    Object.assign(this, init);
  }
}
