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
      setIsLoading(true);
      const response = await fetch(url);
      const responseData = await response.json();
      console.log(responseData);
      setIsLoading(false);

      if (!response.ok) {
        const error = response.message;
        setError(error);
        console.log(error);
        return;
      }

      setPotions(responseData);
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
        <MessageModal message={error} onClose={clearError} itIsAnError />
      ) : (
        <PotionsTable
          potions={potions}
          onDelete={fetchPotions}
          onDeletionStart={() => setIsLoading(true)}
          onDeletionEnd={() => setIsLoading(false)}
          onDeletionError={(errorMessageFromPotionDelete) => setError(errorMessageFromPotionDelete)}
        />
      )}
    </>
  );
};

export default Potions;
