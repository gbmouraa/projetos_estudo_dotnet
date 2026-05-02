import api from "./api";

export interface Book {
  id?: string;
  title: string;
  author: string;
  price: number;
  stock: number;
}

export const bookService = {
  async getAll() {
    try {
      const response = await api.get("/books");
      return response.data;
    } catch (error) {
      console.error("Erro ao buscar livros:", error);
    }
  },

  async getById(id: string) {
    try {
      const response = await api.get(`/books/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao buscar livro ${id}:`, error);
    }
  },

  async create(book: Book) {
    try {
      const response = await api.post("/livros", book);
      return response.data;
    } catch (error) {
      console.error("Erro ao criar livro:", error);
    }
  },

  async update(id: string, book: Partial<Book>) {
    const response = await api.put(`/book/${id}`, book);
    return response.data;
  },

  async delete(id: string) {
    try {
      await api.delete(`/book/${id}`);
    } catch (error) {
      console.error(`Erro ao deletar livro ${id}:`, error);
    }
  },
};
