import "./App.css";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Navbar from "./Components/Navigation/Navbar";
import Home from "./Components/Pages/Home";
import Potions from "./Components/Pages/Potions";
import StartBrewing from "./Components/Pages/StartBrewing";
import AddIngredient from "./Components/Pages/AddIngredient";

function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <Navbar />
        <Routes>
          <Route path="/potions" element={<Potions />} />
          <Route path="/potions/startbrewing" element={<StartBrewing />} />
          <Route path="/potions/:potionId/addingredient" element={<AddIngredient />} />

          <Route path="/" element={<Home />} />
          <Route path="*" element={<Navigate replace to="/" />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
