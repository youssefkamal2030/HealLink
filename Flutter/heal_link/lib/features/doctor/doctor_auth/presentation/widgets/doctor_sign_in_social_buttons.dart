
import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/social_buttons_row.dart';

class DoctorSignInSocialMediaButtons extends StatelessWidget {
  const DoctorSignInSocialMediaButtons({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(child: SocialButtonsRow(
      faceBookOnPressed: (){},
      googleOnPressed: (){},
      appleOnPressed: (){},
    ));
  }
}
