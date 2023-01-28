import { useState, useEffect } from "react";

const SuccessMessage = () => {
  const [message, setMessage] = useState(null);

  const getRandomSuccessMessage = async () => {
    const url = "https://localhost:44390/api/recipes/success";

    try {
      const response = await fetch(url);
      const responseData = await response.json();
      console.log(responseData);

      if (!response.ok) {
        const error = response.message;
        console.log(error);
        return;
      }

      setMessage(responseData);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    getRandomSuccessMessage();
  }, []);

  return (
    <div>
      {message ? message : "Congratulations! You have invented a new Recipe"}
    </div>
  );
};

export default SuccessMessage;
