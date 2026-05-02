import { EditBookDialog } from "./edit-book-dialog";
import type { Book } from "@/services/books";

interface BookItemProps {
  book: Book;
}

export function BookItem({ book }: BookItemProps) {
  return (
    <div className="border-chart-5 relative w-2xs rounded border border-t-3 border-t-green-400 px-4 py-3">
      <p className="font-medium">{book.title}</p>
      <p className="text-chart-1 text-xs">{book.author}</p>
      <div className="mt-4 flex gap-x-3">
        <p className="text-chart-1 text-sm">Preco: R$ {book.price}</p>
        <p className="text-chart-1 text-sm">Estoque:{book.stock}</p>
        <div className="absolute top-3 right-3">
          <EditBookDialog
            id={book.id}
            author={book.author}
            title={book.title}
            price={book.price}
            stock={book.stock}
          />
        </div>
      </div>
    </div>
  );
}
