REM Default start
start KSR.exe

REM Start with
REM -------------------Printing settings------------------------------
REM {
REM BinaryArticleBodyFeature,
REM KeyWordsFirstParagraphArticleBodyFeature,
REM Binary Function,
REM Niewiadomski Function,
REM }
REM Testing percentage: 60, Learning percentage: 40
REM kNN neighbors: 10
REM Chosen keyWordsExtractor: KSR.Tools.Frequency.TFFrequency
REM ------------------------------------------------------------------

start KSR.exe -f1 tfft -f2 tfffft -t 60 -l 40 -kwe 1 -k 10


REM -------------------Printing settings------------------------------
REM {
REM BinaryArticleBodyFeature,
REM SimilarityBodyFeature -> Binary Function,
REM SimliarityFirstParagraph -> Niewiadomski Function,
REM Simliarity30PercentBody -> Jaccard Function,
REM }
REM Testing percentage: 60, Learning percentage: 40
REM kNN neighbors: 10
REM Chosen keyWordsExtractor: KSR.Tools.Frequency.TFFrequency
REM ------------------------------------------------------------------

start KSR.exe -f1 tfff -f2 tfffff -f3 ffffft -f4 ftffff -t 60 -l 40 -kwe 1 -k 10