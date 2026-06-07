import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Field, FieldGroup } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Pencil } from "lucide-react";

import type { Book } from "@/services/books";
import { useContext, useState } from "react";
import { BookContext } from "@/context/book-context";
import { toast } from "sonner";
import { InputGroupAddon } from "./ui/input-group";
import { Spinner } from "./ui/spinner";

interface EditBookDialogProps {
  book: Book;
}

export function EditBookDialog({ book }: EditBookDialogProps) {
  const [open, setOpen] = useState(false);
  const [loading, setLoading] = useState(false);
  const [title, setTitle] = useState(book.title);
  const [author, setAuthor] = useState(book.author);
  const [price, setPrice] = useState(book.price);
  const [stock, setStock] = useState(book.stock);

  const { editBook } = useContext(BookContext);

  const resetValues = () => {
    setTitle(book.title);
    setAuthor(book.author);
    setPrice(book.price);
    setStock(book.stock);
  };

  const handleDeleteBook = async () => {
    try {
      const data: Book = {
        ...book,
        title: title,
        author: author,
        price: price,
        stock: stock,
      };

      await editBook(data.id!, data);
      toast.success(`${data.title} editado com sucesso.`);
      setOpen(false);
    } catch {
      toast.warning(
        "Não foi possível excluir no momento, tente novamente mais tarde.",
      );
      setOpen(false);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog
      open={open}
      onOpenChange={(nextOpen) => {
        if (loading && !nextOpen) {
          return;
        }
        setOpen(nextOpen);
        resetValues();
      }}
    >
      <form>
        <DialogTrigger asChild>
          <Button variant="outline">
            <Pencil />
          </Button>
        </DialogTrigger>
        <DialogContent className="sm:max-w-sm">
          <DialogHeader>
            <DialogTitle>{book.title}</DialogTitle>
            <DialogDescription>
              Edite o livro aqui, clique em salvar quando estiver pronto.
            </DialogDescription>
          </DialogHeader>
          <FieldGroup>
            <Field>
              <Label htmlFor="title">Titulo</Label>
              <Input
                id="title"
                name="title"
                defaultValue={title}
                onChange={(e) => setTitle(e.target.value)}
                readOnly={loading}
              />
            </Field>
            <Field>
              <Label htmlFor="author">Autor</Label>
              <Input
                id="author"
                name="author"
                defaultValue={author}
                onChange={(e) => setAuthor(e.target.value)}
                readOnly={loading}
              />
            </Field>
            <Field>
              <Label htmlFor="price">Preço</Label>
              <Input
                id="price"
                name="price"
                defaultValue={price}
                onChange={(e) => setPrice(parseFloat(e.target.value))}
                readOnly={loading}
              />
            </Field>
            <Field>
              <Label htmlFor="stock">Estoque</Label>
              <Input
                id="stock"
                name="stock"
                defaultValue={stock}
                onChange={(e) => setStock(parseInt(e.target.value))}
                readOnly={loading}
              />
            </Field>
          </FieldGroup>
          <DialogFooter>
            {loading ? (
              <InputGroupAddon align="block-end" className="justify-end">
                <Spinner /> Processando...
              </InputGroupAddon>
            ) : (
              <>
                <DialogClose asChild>
                  <Button variant="outline" onClick={resetValues}>
                    Cancelar
                  </Button>
                </DialogClose>
                <Button type="button" onClick={handleDeleteBook}>
                  Savar
                </Button>
              </>
            )}
          </DialogFooter>
        </DialogContent>
      </form>
    </Dialog>
  );
}
