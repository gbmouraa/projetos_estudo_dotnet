import React, { createContext, useEffect, useState } from "react";
import type { Book } from "@/services/books";
import { bookService } from "@/services/books";

interface BookContextProps {
  books: Book[];
  deleteBook: (id: string) => Promise<void>;
  editBook: (id: string, data: Book) => Promise<void>;
  loading: boolean;
}

export const BookContext = createContext<BookContextProps>({
  books: [],
  deleteBook: async () => {},
  editBook: async () => {},
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

  const deleteBook = async (bookId: string) => {
    await bookService.delete(bookId);
    const booksUpdated = books.filter((b) => b.id !== bookId);
    setBooks(booksUpdated);
  };

  const editBook = async (bookId: string, data: Book) => {
    await bookService.update(bookId, data);
    setBooks((currentBooks) =>
      currentBooks.map((book) => (book.id === bookId ? data : book)),
    );
  };

  return (
    <BookContext.Provider value={{ books, deleteBook, loading, editBook }}>
      {children}
    </BookContext.Provider>
  );
}
