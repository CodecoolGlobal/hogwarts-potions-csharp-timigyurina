import { useState, useEffect } from "react";
import PotionsTable from "../PotionElements/PotionsTable";
import LoadingSpinner from "../UIElements/LoadingSpinner";
import MessageModal from "../UIElements/MessageModal";

const Potions = () => {
  const [potions, setPotions] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState();

  const fetchPotions = async () => {
    const url = "https://localhost:44390/api/potions";
    try {
      const response = await fetch(url);
      const data = await response.json();

      if (!response.ok) {
        const error = response.message;
        setIsLoading(false);
        setError(error);
        console.log(error);
      }
      setPotions(data);
      console.log(data);
    } catch (err) {
      setIsLoading(false);
      setError(err.message);
      console.log(err);
    }
  };

  useEffect(() => {
    fetchPotions();
  }, []);

  const clearError = () => {
    setError(null);
  };

  return (
    <>
      {isLoading ? (
        <div className="center">
          <LoadingSpinner asOverlay />
        </div>
      ) : error ? (
        <MessageModal message={error} onClear={clearError} itIsAnError />
      ) : (
        <PotionsTable potions={potions} />
      )}
    </>
  );
};

export default Potions;
