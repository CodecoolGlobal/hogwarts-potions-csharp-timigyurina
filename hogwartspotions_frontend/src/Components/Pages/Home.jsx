import { Box, Typography } from "@mui/material";
import ScienceIcon from "@mui/icons-material/Science";

const Home = () => {
  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        height: "90vh",
      }}
    >
      <h1>Welcome to Hogwarts!</h1>
      <Typography
        variant="body1"
        component="div"
        padding={3}
        textAlign="center"
      >
        <p>
          You can see your list of Potions by clicking on the "Potions" button.{" "}
        </p>
        <p>
          On that page, you can start brewing a new Potion or edit the ones you
          have already created.{" "}
        </p>
        <p>
          Once you start adding Ingredients to your Potion, you may discover new
          Recipes. These can be managed on the "Recipes" page.
        </p>
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            gap: 1,
          }}
        >
          Happy brewing! <ScienceIcon />
        </Box>
      </Typography>
    </Box>
  );
};

export default Home;
