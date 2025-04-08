export interface IUser {
  id: string;
  userImageLink: string;
  userName: string;
}

export interface IRole {
  name: string;
  role: string;
}

export interface IGeneralRecipe {
  id: string;
  title: string;
  description: string;
  imageUrl: string;
  updatedAt: Date;
}
