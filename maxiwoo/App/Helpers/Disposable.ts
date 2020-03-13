export interface IDisposable {
  dispose(): void;
}

export function using<T extends IDisposable>(resource: T, func: (resource: T) => void) {
  try {
    func(resource);
  } finally {
    resource.dispose();
  }
}

export async function usingAsync<T extends IDisposable, TResult>(resource: T, func: (resource: T) => Promise<TResult>): Promise<TResult> {
  try {
    return await func(resource);
  } finally {
    resource.dispose();
  }
}
