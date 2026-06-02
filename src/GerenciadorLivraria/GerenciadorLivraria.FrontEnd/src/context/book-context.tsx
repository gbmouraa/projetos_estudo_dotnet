import React, { createContext, useEffect, useState } from "react";
import type { Book } from "@/services/books";
import { bookService } from "@/services/books";

interface BookContextProps {
  books: Book[];
  handleDeleteBook: (id: string) => Promise<void>;
  loading: boolean;
}

export const BookContext = createContext<BookContextProps>({
  books: [],
  handleDeleteBook: async () => {},
  loading: false,
});

export default function BookContextProvider({
  children,
}: {
  children: React.ReactNode;
}) {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(false);

  async function getBooks() {
    setLoading(true);
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

  const handleDeleteBook = async (bookId: string) => {
    await bookService.delete(bookId);
    const booksUpdated = books.filter((b) => b.id !== bookId);
    setBooks(booksUpdated);
  };

  return (
    <BookContext.Provider value={{ books, handleDeleteBook, loading }}>
      {children}
    </BookContext.Provider>
  );
}
