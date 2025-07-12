import 'package:flutter/cupertino.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/widgets/custom_elevated_button.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorVerifyEmailButton extends StatelessWidget {
  const DoctorVerifyEmailButton({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: CustomElevatedButton(
        text: S.of(context).sign_in,
        onPressed: () {
          context.push(AppRouter.doctorSignInView);
        },
      ),
    );
  }
}
