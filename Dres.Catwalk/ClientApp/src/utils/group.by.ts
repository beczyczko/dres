export class Group<T> {
  constructor(public key: string, public values: T[]) {
  }
}

export const groupBy = <T>(array: T[], predicate: (value: T, index: number, array: T[]) => string) => {
  const groups = array.reduce((acc, value, index, array) => {
    (acc[predicate(value, index, array)] ||= []).push(value);
    return acc;
  }, {} as { [key: string]: T[] });
  return Object.entries(groups).map(([key, values]) => new Group<T>(key, values));
};
