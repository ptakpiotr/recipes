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
  imageUrl?: string;
  updatedAt: Date;
}

export enum RecipeType {
  Sweet,
  Dinner,
  Breakfast,
  Lunch,
  Other,
}

export interface IIngredientReadDto {
  id: string;
  recipeId: string;
  description: string;
  order: number;
}

export interface IRatingReadDto {
  id: string;
  rating: number;
  recipeId: string;
  userId: string;
}

export interface IRatingCreateDto {
  rating: number;
  recipeId: string;
}

export interface IRecipe extends IGeneralRecipe {
  authorId: string;
  types: RecipeType[];
  createdAt: string;
  ingredients: IIngredientReadDto[];
  ratings: IRatingReadDto[];
}

export interface IRecipeEditDto extends Omit<IGeneralRecipe, "imageUrl"> {
  authorId: string;
  types: RecipeType[];
  ingredients: IIngredientReadDto[];
}

export interface IIngredientManageDto {
  readonly recipeId: string;
  description: string;
  order: number;
}

export type CreateIngredientDto = Omit<IIngredientManageDto, "recipeId">;

interface IUserBasicReadStandard {
  id: string;
  userName: string;
  userImageLink: string;
}

export type UserBasicReadDto = Readonly<IUserBasicReadStandard>;

export interface IRoleReadDto {
  readonly id: string;
  readonly name: string;
  readonly role: RoleType;
}

export enum RoleType {
  User,
  Admin,
}
