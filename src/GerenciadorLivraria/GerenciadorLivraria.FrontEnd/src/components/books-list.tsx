import { useEffect, useState } from "react";
import { EditBookDialog } from "./edit-book-dialog";
import { LoadingSpinner } from "./loading-spinner";

export interface BookInterface {
  id: string;
  title: string;
  author: string;
  price: number;
  stock: number;
}

export function BooksList() {
  const [books, setBooks] = useState<BookInterface[]>([]);
  const [loading, setLoading] = useState(true);

  async function getBooks() {
    try {
      const apiUrl = "http://localhost:5139/api/book";
      const response = await fetch(apiUrl);

      if (!response.ok) {
        throw new Error("Erro ao buscar livros.");
      }

      const data: BookInterface[] = await response.json();
      setBooks(data);
    } catch (ex) {
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
        <div className="-trasnla fixed top-1/2">
          <LoadingSpinner text="Carregando Livros" />
        </div>
      ) : books.length > 0 ? (
        <div className="mx-auto mt-20 px-3">
          <h1 className="mb-6 text-xl font-bold">Estande de Livros</h1>
          <ul className="flex w-full max-w-7xl flex-wrap gap-6">
            {books.map((b) => (
              <li key={b.id}>
                {/* TODO: Componentizar Livros */}
                <div className="border-chart-5 relative w-2xs rounded border border-t-3 border-t-green-400 px-4 py-3">
                  <p className="font-medium">{b.title}</p>
                  <p className="text-chart-1 text-xs">{b.author}</p>
                  <div className="mt-4 flex gap-x-3">
                    <p className="text-chart-1 text-sm">Preco: R$ {b.price}</p>
                    <p className="text-chart-1 text-sm">Estoque:{b.stock}</p>
                    <div className="absolute top-3 right-3">
                      <EditBookDialog
                        id={b.id}
                        author={b.author}
                        title={b.title}
                        price={b.price}
                        stock={b.stock}
                      />
                    </div>
                  </div>
                </div>
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
