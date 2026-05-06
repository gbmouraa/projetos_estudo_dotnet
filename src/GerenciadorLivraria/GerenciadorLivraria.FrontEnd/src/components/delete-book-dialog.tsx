import { useState } from "react";
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
import { Trash } from "lucide-react";
import { Spinner } from "./ui/spinner";
import { InputGroupAddon } from "./ui/input-group";
import { bookService } from "../services/books";
import { toast } from "sonner";

interface DeleteBookDialogProps {
  id: string;
  title: string;
  author: string;
}

export function DeleteBookDialog({ id, title, author }: DeleteBookDialogProps) {
  const [open, setOpen] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleDeleteBook = async (bookId: string) => {
    setLoading(true);
    try {
      await bookService.delete(bookId);
      toast.success(`${title} excluido com sucesso.`);
      setOpen(false);
    } catch {
      toast.warning(
        "Não foi possível esxluir no momento, tente novamente mais tarde.",
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
      }}
    >
      <form>
        <DialogTrigger asChild>
          <Button variant="outline">
            <Trash />
          </Button>
        </DialogTrigger>
        <DialogContent showCloseButton={!loading} className="sm:max-w-sm">
          <DialogHeader>
            <DialogTitle>{title}</DialogTitle>
            <small className="text-chart-1 text-xs">{author}</small>
            <DialogDescription>
              {loading ? (
                <span>Você escolheu exluir este livro da sua livaria.</span>
              ) : (
                <span>Tem certeza que deseja excluir esse livro?</span>
              )}
            </DialogDescription>
          </DialogHeader>
          <DialogFooter>
            {loading ? (
              <InputGroupAddon align="block-end" className="justify-end">
                <Spinner /> Processando...
              </InputGroupAddon>
            ) : (
              <>
                <DialogClose asChild>
                  <Button type="button" variant="outline">
                    Cancelar
                  </Button>
                </DialogClose>
                <Button type="button" onClick={() => handleDeleteBook(id)}>
                  Excluir
                </Button>
              </>
            )}
          </DialogFooter>
        </DialogContent>
      </form>
    </Dialog>
  );
}
