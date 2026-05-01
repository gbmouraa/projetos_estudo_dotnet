import { BooksList } from "./components/books-list";
import { Header } from "./components/header";

function App() {
  return (
    <div className="bg-primary-foreground min-h-screen w-full">
      <Header />
      <BooksList />
    </div>
  );
}

export default App;
