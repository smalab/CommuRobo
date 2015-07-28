#import "EasyTTSUtil.h"


@implementation EasyTTSUtil

@synthesize synthesizer;
- (AVSpeechSynthesizer *)synthesizer {
    if (synthesizer == nil) {
        synthesizer = [[AVSpeechSynthesizer alloc] init];
    }
    return synthesizer;
}

- (void) convertTextToSpeech:(NSString *)text local:(NSString*)local{
    AVSpeechUtterance *utterance = [[AVSpeechUtterance alloc] initWithString:text];
    utterance.rate = 0.12f;
    utterance.voice = [AVSpeechSynthesisVoice voiceWithLanguage:local];
    [self.synthesizer speakUtterance:utterance];
}
- (void) stopSpeech {
    [self.synthesizer stopSpeakingAtBoundary:AVSpeechBoundaryImmediate];
}

@end

extern "C" {
    void EasyTTSUtilSpeech(char* text,char* local);
    void EasyTTSUtilStop();

}


static EasyTTSUtil *tts = nil;


void EasyTTSUtilSpeech(char* text,char* _local)
{
    if(tts == nil)
    {
        tts = [EasyTTSUtil alloc];
    }
    NSString *sName = (text != nil) ? [NSString stringWithUTF8String: text] : [NSString stringWithUTF8String: ""];
    NSString *slocal = (text != nil) ? [NSString stringWithUTF8String: _local] : [NSString stringWithUTF8String: ""];
    [tts convertTextToSpeech:sName local:slocal];
}

void EasyTTSUtilStop()
{
    if(tts != nil)
    {
        [tts stopSpeech];
    }
    
}
