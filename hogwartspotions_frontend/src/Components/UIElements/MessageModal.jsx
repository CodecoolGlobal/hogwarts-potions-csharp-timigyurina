import { useState } from "react";
import ReactDOM from "react-dom";

import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import ModalBase from "./ModalBase";

const style = {
  display: "flex",
  flexDirection: "column",
  alignItems: "center",
  gap: "15px",
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid black",
  boxShadow: 24,
  p: 4,
};

const MessageModal = ({
  message,
  onClose,
  onGoTo,
  moreMessage,
  buttonText = "Close",
  itIsAnError,
}) => {
  const [open, setOpen] = useState(true);

  const handleClose = () => {
    onClose();
    setOpen(false);
  };

  const handleGoTo = () => {
    onGoTo();
    setOpen(false);
  };

  const content = (
    <div>
      <ModalBase open={open} handleClose={handleClose} boxStyle={style}>
        <Typography
          id="transition-modal-title"
          variant="h6"
          component="h2"
          sx={itIsAnError && { color: "#c62828" }}
        >
          {message}
        </Typography>
        {moreMessage && (
          <Typography id="transition-modal-description" sx={{ mt: 2 }}>
            {moreMessage}
          </Typography>
        )}
        <div className="modal-buttons-container">
          <Button
            onClick={handleClose}
            variant="outlined"
            color={itIsAnError ? "error" : "primary"}
          >
            Close
          </Button>
          {!itIsAnError && (
            <Button onClick={handleGoTo} variant="outlined" color={"primary"}>
              {buttonText}
            </Button>
          )}
        </div>
      </ModalBase>
    </div>
  );

  return ReactDOM.createPortal(content, document.getElementById("modal-hook"));
};

export default MessageModal;
