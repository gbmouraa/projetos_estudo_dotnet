import { BooksList } from "./components/books-list";
import { Header } from "./components/header";
import { Toaster } from "@/components/ui/sonner";

function App() {
  return (
    <div className="bg-primary-foreground min-h-screen w-full">
      <Header />
      <BooksList />
      <Toaster position="top-center" />
    </div>
  );
}

export default App;
