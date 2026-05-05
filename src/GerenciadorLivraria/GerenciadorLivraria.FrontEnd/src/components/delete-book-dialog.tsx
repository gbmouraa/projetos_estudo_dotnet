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

interface DeleteBookDialogProps {
  id: string;
  title: string;
  author: string;
}

export function DeleteBookDialog({ id, title, author }: DeleteBookDialogProps) {
  const [open, setOpen] = useState(false);
  const [loading, setLoading] = useState(false);
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
                <span>Voce escolheu exluir este livro da sua livaria</span>
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
                  <Button variant="outline">Cancelar</Button>
                </DialogClose>
                <Button onClick={() => setLoading(!loading)}>Excluir</Button>
              </>
            )}
          </DialogFooter>
        </DialogContent>
      </form>
    </Dialog>
  );
}
