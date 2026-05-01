import Logo from "@/assets/logo.png";
import LinkedinIcon from "@/assets/linkedin-icon.png";
import GithubIcon from "@/assets/github-icon.png";
import { Input } from "./ui/input";

export function Nav() {
  return (
    <nav className="relative flex flex-1 items-center">
      <div className="flex items-center">
        <div className="mr-2">
          <img src={Logo} alt="Logo" width={23} className="block" />
        </div>
        <a
          href="#"
          className="hover:bg-chart-2 cursor-pointer rounded p-1 text-xs text-white"
        >
          Adicionar
        </a>
      </div>
      <Input className="bg-primary-foreground border-accent/10 absolute left-1/2 h-5.5 max-w-100 -translate-x-1/2 rounded-sm text-white" />
      <div className="fixed right-2 flex gap-4">
        <a href="https://www.linkedin.com/in/gabriel-moura-b63382161/?skipRedirect=true">
          <img
            src={LinkedinIcon}
            alt="Linkedin Icon"
            className="opacity-40 transition-opacity hover:opacity-100"
            width={18}
          />
        </a>
        <a href="https://github.com/gbmouraa" target="_blank">
          <img
            src={GithubIcon}
            alt="Github Icon"
            width={18}
            className="opacity-40 transition-opacity hover:opacity-100"
          />
        </a>
      </div>
    </nav>
  );
}
