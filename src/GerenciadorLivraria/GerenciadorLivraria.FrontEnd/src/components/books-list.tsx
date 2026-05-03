import { useEffect, useState } from "react";
import { BookItem } from "./book-item";
import { LoadingSpinner } from "./loading-spinner";
import type { Book } from "@/services/books";
import { bookService } from "@/services/books";

export function BooksList() {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(true);

  async function getBooks() {
    try {
      const response = (await bookService.getAll()) as Book[];
      setBooks(response);
    } catch (ex) {
      // configurar msg de erro na tela
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    getBooks();
  }, []);

  return (
    <section className="flex w-full justify-center">
      {loading ? (
        <div className="fixed top-1/2">
          <LoadingSpinner text="Carregando Livros" />
        </div>
      ) : books.length > 0 ? (
        <div className="mx-auto mt-20 px-3">
          <h1 className="mb-6 text-xl font-bold">Estande de Livros</h1>
          <ul className="flex w-full max-w-7xl flex-wrap gap-6">
            {books.map((b) => (
              <li key={b.id}>
                <BookItem book={b} />
              </li>
            ))}
          </ul>
        </div>
      ) : (
        <div className="fixed top-1/2 flex flex-col justify-center text-center text-white">
          <p>Ops, parece que sua livraria esta vazia 🥲</p>
          <a
            href=""
            className="inline-block text-sm text-blue-400 hover:underline"
          >
            Adicionar um livro
          </a>
        </div>
      )}
    </section>
  );
}
