export type DotNetObjectType = {
  invokeMethodAsync<T>(methodIdentifier: string, ...arguments_: unknown[]): Promise<T>
  _id: number
}
