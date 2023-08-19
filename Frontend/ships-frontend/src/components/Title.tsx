import { TitleModel } from '../models/TitleModel';

export default function Title({text}: TitleModel) {

  function getClassForWinnerText(): string | undefined {
    if (text === "Player 1") {
      return "red";
    }
    if (text === "Player 2") {
      return "blue";
    }
    return undefined;
  }

  return (
    <div className='title'>
      <span className='title-text'>Ships in .NET + REACT</span>
      {text && (
        <span className='title__winner'>
          The Winner is
          <span className={`winner ${getClassForWinnerText()}`}>
            {text}
          </span>
        </span>
      )}
    </div>
  )
}
