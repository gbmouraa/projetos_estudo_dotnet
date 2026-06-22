import { z } from "zod";

export const bookSchema = z.object({
  title: z.string().min(1, "Titulo não pode ser vazio."),
  author: z.string().min(1, "Autor não pode ser vazio."),
  price: z.number().positive("Insira um preço válido."),
  stock: z.number().min(0, "Estoque não pode ser negativo."),
});

export type BookFormData = z.infer<typeof bookSchema>;
