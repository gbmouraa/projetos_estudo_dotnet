import { Item, ItemContent, ItemMedia, ItemTitle } from "@/components/ui/item";
import { Spinner } from "@/components/ui/spinner";

interface LoadingSpinnerProps {
  text: string;
}

export function LoadingSpinner({ text }: LoadingSpinnerProps) {
  return (
    <div className="flex w-full max-w-xs flex-col items-center gap-4 [--radius:1rem]">
      <Item variant="muted" className="w-fit">
        <ItemMedia>
          <Spinner />
        </ItemMedia>
        <ItemContent>
          <ItemTitle className="line-clamp-1">{text}...</ItemTitle>
        </ItemContent>
      </Item>
    </div>
  );
}
