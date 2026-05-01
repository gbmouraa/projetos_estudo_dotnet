import api from "./api";

export interface Livro {
  id?: string;
  titulo: string;
  autor: string;
  descricao: string;
  preco: number;
  dataCriacao?: Date;
}

export const livrosService = {
  // Obter todos os livros
  async getAll() {
    try {
      const response = await api.get("/livros");
      return response.data;
    } catch (error) {
      console.error("Erro ao buscar livros:", error);
      throw error;
    }
  },

  // Obter livro por ID
  async getById(id: string) {
    try {
      const response = await api.get(`/livros/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao buscar livro ${id}:`, error);
      throw error;
    }
  },

  // Criar novo livro
  async create(livro: Livro) {
    try {
      const response = await api.post("/livros", livro);
      return response.data;
    } catch (error) {
      console.error("Erro ao criar livro:", error);
      throw error;
    }
  },

  // Atualizar livro
  async update(id: string, livro: Partial<Livro>) {
    try {
      const response = await api.put(`/livros/${id}`, livro);
      return response.data;
    } catch (error) {
      console.error(`Erro ao atualizar livro ${id}:`, error);
      throw error;
    }
  },

  // Deletar livro
  async delete(id: string) {
    try {
      await api.delete(`/livros/${id}`);
    } catch (error) {
      console.error(`Erro ao deletar livro ${id}:`, error);
      throw error;
    }
  },
};
