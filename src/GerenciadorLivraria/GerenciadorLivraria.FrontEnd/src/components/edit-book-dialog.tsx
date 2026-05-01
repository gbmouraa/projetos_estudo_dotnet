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

import type { BookInterface } from "./books-list";

export function EditBookDialog({ title, author, price, stock }: BookInterface) {
  return (
    <Dialog>
      <form>
        <DialogTrigger asChild>
          <Button variant="outline">
            <Pencil />
          </Button>
        </DialogTrigger>
        <DialogContent className="sm:max-w-sm">
          <DialogHeader>
            <DialogTitle>{title}</DialogTitle>
            <DialogDescription>
              Edite o livro aqui, clique em salvar quando estiver pronto.
            </DialogDescription>
          </DialogHeader>
          <FieldGroup>
            <Field>
              <Label htmlFor="title">Titulo</Label>
              <Input id="title" name="title" defaultValue={title} />
            </Field>
            <Field>
              <Label htmlFor="author">Autor</Label>
              <Input id="author" name="author" defaultValue={author} />
            </Field>
            <Field>
              <Label htmlFor="price">Preco</Label>
              <Input id="price" name="price" defaultValue={price} />
            </Field>
            <Field>
              <Label htmlFor="stock">Estoque</Label>
              <Input id="stock" name="stock" defaultValue={stock} />
            </Field>
          </FieldGroup>
          <DialogFooter>
            <DialogClose asChild>
              <Button variant="outline">Cancelar</Button>
            </DialogClose>
            <Button type="submit">Savar</Button>
          </DialogFooter>
        </DialogContent>
      </form>
    </Dialog>
  );
}
