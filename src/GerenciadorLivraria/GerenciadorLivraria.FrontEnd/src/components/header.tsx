import Logo from "@/assets/logo.png";
import { Input } from "./ui/input";

export function Header() {
  return (
    <header className="bg-foreground/70 flex h-8 w-full items-center px-2">
      {/* TODO: Componentizar nav */}
      <nav className="relative flex flex-1 items-center">
        <div className="flex items-center">
          <div className="mr-2">
            <img src={Logo} alt="Logo" width={23} className="block" />
          </div>
          <a
            href="#"
            className="hover:bg-accent/50 cursor-pointer rounded p-1 text-xs text-white"
          >
            Adicionar
          </a>
        </div>
        <Input className="bg-accent-foreground border-accent/10 absolute left-1/2 h-5.5 max-w-[400px] -translate-x-1/2 rounded-sm text-white" />
      </nav>
    </header>
  );
}
